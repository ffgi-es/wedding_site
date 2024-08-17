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
    location          = azurerm_resource_group.location
    failover_priority = 0
  }

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.example.id]
  }
}

resource "azurerm_cosmosdb_sql_database" "wedding_db" {
  name                = "wedding"
  resource_group_name = azurerm_cosmosdb_account.wedding_cosmos_account.resource_group_name
  account_name        = azurerm_cosmosdb_account.wedding_cosmos_account.name
}

resource "azurerm_cosmosdb_sql_container" "example" {
  name                  = "rsvp"
  resource_group_name   = azurerm_cosmosdb_sql_database.wedding_db.resource_group_name
  account_name          = azurerm_cosmosdb_sql_database.wedding_db.name
  database_name         = azurerm_cosmosdb_sql_database.wedding_db.name
  partition_key_paths   = ["/id"]
  partition_key_version = 1
  throughput            = 400

  indexing_policy {
    indexing_mode = "consistent"

    included_path {
      path = "/id"
    }

    excluded_path {
      path = "/*"
    }
  }

  unique_key {
    paths = ["/definition/idlong", "/definition/idshort"]
  }
}