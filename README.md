# Enterprise DevOps Training

## [PREREQ] Clone the Application Repository
1. Create a new Azure account
1. Create a new Azure organization & project. Save the Repos Url.
1. Clone the Github repo (https://github.com/bendavismines/devOpsWorkshop)
1. Add a new remote `git remote add my_awesome_new_remote_name https://CrederaDevOps@dev.azure.com/CrederaDevOps/Test-Remote-Add/_git/Test-Remote-Add`
1. Push code up to new repo! `git push -u my_awesome_new_remote_name --all`

## Provisioning Azure Resources
Create the following Azure resources in the [Azure Portal](https://portal.azure.com/#home). 

**Notes for Success**
- **All resources should be created in the same region.**
- **Resource Groups are a logical way to organize related cloud resources**
- **Use the free versions of these Azure resources**

### Web Apps (In App Services)
1. Create a Linux Web App with a .Net Core runtime stack for your dev environment
1. Create a Linux Web App with a .Net Core runtime stack for your production environment

_We recommend that you give your dev and prod environments the same name, but give dev a "-dev" suffix._

Deeper Dive
- Why do you have use different environments? 

  Production environments are intended for the public facing application. 
  Development environments are usually intended for integrating all dev changes together.
  Test/Staging environments are a stable environment that is as close to production-like as possible to run final tests before deployment.

## Setting Up Build Pipeline 
1. Create a new Build Pipeline in your Azure DevOps Project
1. Use the Starter pipeline template as your starting point

**Notes for Success**
- **Use the Show Assistant option under the "Save and run" button**
- **Your cursor placement in the yaml file determines where the assistant pastes build steps**

###  Update the Build Pipeline to Run Tests and Publish Results
1. Add a new .Net Core Test task in your Build Pipeline that will run before the publish script
1. Pass in the path to the .csproj associated with the test project
1. Make sure the task publishes the test results and code coverage

### Add a Step to Build/Publish your .Net Application
1. Add a new .Net Core Publish task in your build pipeline that will build & publish your Application
1. Specify the --output argument to output the final build to the `$(Build.ArtifactStagingDirectory)` path

### Add a Publish Build Artifact Task
1. Add a new Publish Build Artifacts Task to publish the contents of `$(Build.ArtifactStagingDirectory)`

### Generate the Pipeline Configuration
1. Save the pipeline and commit the new pipeline script to your repository
1. Run the pipeline!  

## Setting Up Branch Policies
1. Navigate to the "Branches" page for your repo
1. Use the ellipses to access the "Branch Policy" page for your Master branch
1. Add a build validation policy which triggers the new build pipeline you've created
1. Test your work! Create a branch off of master, modify UnitTest1.cs to cause a failure (Change the assert), push the branch to your repo, and create a pull request into master. Watch the build pipeline try to compile your code and run tests.

Deeper Dive
- Why should you use branch policies and what other types are there?

  In this tutorial we are creating a branch policy to ensure code is integrated with target branch and tested before it can be merged into master to help limit release of broken code. Other branch policies are number of approvals on the merge request, linking work items from the backlog, ensuring all comments have been resolved (good use when using code linters) or limiting merge types (requiring squashing of commits or rebasing)

## Release Pipeline & Environment Variables
1. In the Pipelines > Releases section, create a new Release Pipeline

### Add Artifact
1. Add a build artifact and specify the Build Pipeline you created 

### Create a Dev Stage
1. Add a Deployment process Stage for Development
1. Create a new Azure Web App Deploy Task for the Dev Stage
1. In the new Task specify the correct directory for your artifact in the "Package or Folder" setting *(browse the artifact filepath to understand what is being published)*
1. Specify the Web App as the dev Web App created at the beginning of the tutorial
1. For the startup command use `dotnet NeatProject.Api.dll` 
1. Set the `ASPNETCORE_ENVIRONMENT` variable to the name of your intended appsettings.json environment file to load
1. Test the Release! Run the release pipeline and visit your Dev Web App url and hit the /weatherforecast endpoint 

Deeper Dive
- What is the Agent Job?

  Pipelines require a build agent that will execute the build or deployment steps that are being configured in Azure DevOps. Build agents can be Linux, MacOS, or Windows machines. The specification required depends on what you are building in your pipeline.

- What are the environment variables that are created?

  *How do we find the name needed?*

- What is the App Settings File?

  There is a root appsettings.json and appsettings.{env}.json file for each environment that is being loaded according the value of the `ASPNETCORE_ENVIRONEMNT` variable. The example we have is logging. A user may want to log all warnings and information logs in the Dev environment, but the minimize noise only errors will be logged in Production. 

## Setting Up Production Pipeline
1. Create a Production Stage in your Release Pipeline in the same way you created the dev stage but this time release to your Production Web App with the correct `ASPNETCORE_ENVIRONMENT` variable
1. Test the Release! Run the release pipeline and visit your Production Web App url and hit the /weatherforecast endpoint 

## Test it out! 
1. On the branch you created earlier, change a value in the WeatherForecastController, change your test back to passing and push it up
1. Merge the Pull Request into master when the PR has passed
1. View the updated application on your dev server and your production server 

## Extra Credit 

### Azure App Config
1. Provision an Azure App Config resource
1. Create new configuration values for the dev environment 
1. Create new configuration values for the production environment
1. Use them configuration values in your pipeline! 

### Azure Key Vault
1. Provision an Azure Key Vault 
1. Store App Config connection string in Key Vault 
1. Modify Release pipeline to add in a step to pull values from key vault
