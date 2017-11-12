# About
This is an Azure Function written in C# that will generate a [Buddhabrot Fractal](https://en.wikipedia.org/wiki/Buddhabrot). The function is triggered with a HTTP request and returns the fractal as a png image.

# Live Demo
https://buddhabrot.azurewebsites.net/api/Buddhabrot

# Spin up your own Buddhabrot Azure Function
* Clone this repo
* Create an Azure Function manually in the portal or <a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FDonKarlssonSan%2FBuddhabrotAzureFunction%2Fmaster%2Fazuredeploy.json">click here
</a>to start a deployment wizard for creating a dynamic app service plan, a storage account and a Function App.
* [Configure GitHub deployment](https://docs.microsoft.com/en-us/azure/azure-functions/functions-continuous-deployment)
