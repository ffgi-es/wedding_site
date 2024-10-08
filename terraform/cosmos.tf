resource "azurerm_user_assigned_identity" "wedding_identity" {
  resource_group_name = azurerm_resource_group.wedding_site.name
  location            = azurerm_resource_group.wedding_site.location
  name                = "example-resource"
}

resource "azurerm_cosmosdb_account" "wedding_cosmos_account" {
  name                  = "wedding-cosmos"
  location              = azurerm_resource_group.wedding_site.location
  resource_group_name   = azurerm_resource_group.wedding_site.name
  default_identity_type = join("=", ["UserAssignedIdentity", azurerm_user_assigned_identity.wedding_identity.id])
  offer_type            = "Standard"

  consistency_policy {
    consistency_level = "Strong"
  }

  geo_location {
    location          = azurerm_resource_group.wedding_site.location
    failover_priority = 0
  }

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.wedding_identity.id]
  }
}

resource "azurerm_cosmosdb_sql_database" "wedding_db" {
  name                = "wedding"
  resource_group_name = azurerm_cosmosdb_account.wedding_cosmos_account.resource_group_name
  account_name        = azurerm_cosmosdb_account.wedding_cosmos_account.name
}

resource "azurerm_cosmosdb_sql_container" "rsvp_container" {
  name                  = "rsvp"
  resource_group_name = azurerm_cosmosdb_account.wedding_cosmos_account.resource_group_name
  account_name        = azurerm_cosmosdb_account.wedding_cosmos_account.name
  database_name         = azurerm_cosmosdb_sql_database.wedding_db.name
  partition_key_paths   = ["/partition"]
  partition_key_version = 1
  throughput            = 400

  unique_key {
    paths = ["/confirmationCode"]
  }
}