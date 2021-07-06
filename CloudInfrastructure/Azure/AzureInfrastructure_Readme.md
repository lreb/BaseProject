# Azure

## Configuration

- [Install Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)

`az account list`

`az account set --subscription <SUBSCRIPTION_NAME>`

## Get Locations

Table list
`az account list-locations -o table`

Sort by display name
`az account list-locations --query "sort_by([].{DisplayName:displayName, Name:name}, &DisplayName)" --output table`

Detailes
`az account list-locations`

## Deployment scope

To deploy to a resource group,
`az deployment group create --resource-group <resource-group-name> --template-file <path-to-template>`

To deploy to a subscription
`az deployment sub create --location <location> --template-file <path-to-template>`

To deploy to a management group
`az deployment mg create --location <location> --template-file <path-to-template>`

To deploy to a tenant
`az deployment tenant create --location <location> --template-file <path-to-template>`

## Resources

### Resource Group

Get tenanat id with `az account list`

`az deployment sub create --template-file .\ResourceGroup.json --location westus2 --parameters rgName=BaseProject rgLocation=westus2 rgEnvironment="Development"`

### Key Vault

NOTE: remember to create a service principal (register your application scope) the we can retrieve the principal id with `az ad sp list --display-name <Sevice_Principal_Name>`

Retrieve tenant `az account list`

az deployment group create --resource-group BaseProject-Development --template-file .\KeyVault.json --parameters name=kvtest principalId=d556829a-6919-4fc6-8551-ff135e84f8a6

az deployment group create --resource-group <resource-group-name> --template-file <path-to-template>

az deployment group create --resource-group rgtest --template-file .\ResourceGroup.json --parameters rgName=BaseProject rgLocation=westus2 rgEnvironment="Development"