resource "azurerm_resource_group" "wedding_site" {
  name     = "wedding-site"
  location = "West Europe"
}

resource "azurerm_service_plan" "wedding_site" {
  name                = "wedding-site-plan"
  resource_group_name = azurerm_resource_group.wedding_site.name
  location            = azurerm_resource_group.wedding_site.location
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "wedding_site" {
  name                = "emilie-alastair-wedding"
  resource_group_name = azurerm_resource_group.wedding_site.name
  location            = azurerm_resource_group.wedding_site.location
  service_plan_id     = azurerm_service_plan.wedding_site.id

  site_config {
    always_on = false
    application_stack {
      docker_image_name        = "${var.docker_username}/wedding_site:${var.image_tag}"
      docker_registry_url      = "https://index.docker.io"
      docker_registry_username = var.docker_username
      docker_registry_password = var.docker_token
    }

    websockets_enabled = true
    use_32_bit_worker  = false
  }
}