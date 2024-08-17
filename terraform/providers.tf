provider "azurerm" {
  features {}
}

terraform {
  backend "azurerm" {
    resource_group_name  = "terraform"
    storage_account_name = "tfstate29597"
    container_name       = "tfstate"
    key                  = "prod.terraform.tfstate"
    use_oidc             = true # Can also be set via `ARM_USE_OIDC` environment variable.
    use_azuread_auth     = true # Can also be set via `ARM_USE_AZUREAD` environment variable.
  }
}
