# Release Plan

## Versioning strategy

Coverlet is versioned with Semantic Versioning [2.0.0](https://semver.org/#semantic-versioning-200) that states:

```text
Given a version number MAJOR.MINOR.PATCH, increment the:

MAJOR version when you make incompatible API changes,
MINOR version when you add functionality in a backwards-compatible manner, and
PATCH version when you make backwards-compatible bug fixes.
Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.
```

We release 4 components as NuGet packages:

* **coverlet.msbuild.nupkg**
* **coverlet.console.nupkg**
* **coverlet.collector.nupkg**
* **coverlet.MTP.nupkg**

## How to manually compare latest release with nightly build

Before creating a new release it makes sense to test the new release against a benchmark repository. This can help to determine bugs that haven't been found
by the unit/integration tests. Therefore, coverage of the latest release is compared with our nightly build.

In the following example the benchmark repository refit (<https://github.com/reactiveui/refit>) is used which already uses coverlet for coverage.

1. Clone the benchmark repository (<https://github.com/reactiveui/refit>)
2. Check if latest coverlet version is used by the project, otherwise add coverlet to the project (<https://github.com/coverlet-coverage/coverlet#installation>).
3. Create coverage report for latest coverlet version:

    ```shell
    dotnet test --collect:"XPlat Code Coverage"
    ```

4. Update the test projects with the latest nightly build version of coverlet
(<https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/ConsumeNightlyBuild.md>).

5. Create coverage report for nightly build version by rerunning the tests:

    ```shell
    dotnet test --collect:"XPlat Code Coverage"
    ```

6. Check for differences in the coverage reports.

## Configure the `Sign Client Credentials` variable group

The nightly pipeline reads its Azure Key Vault signing credentials from an Azure DevOps variable group with this exact name. Create the group in the `coverlet` Azure DevOps project and authorize only the nightly pipeline to use it.

1. Install Azure CLI, then sign in to the Azure DevOps organization:

    ```shell
    az login
    az extension add --name azure-devops --upgrade
    az devops configure --defaults organization=https://dev.azure.com/<organization> project=coverlet
    ```

    Replace `<organization>` with the Azure DevOps organization name. The account must have permission to create variable groups in the project. The Azure DevOps CLI extension requires Azure CLI 2.30.0 or later.

2. Create the group with the non-secret settings and a temporary value for the client secret:

    ```shell
    az pipelines variable-group create \
      --name "Sign Client Credentials" \
      --description "Azure Key Vault credentials used to sign nightly NuGet packages." \
      --variables \
        SignKeyVaultUrl=https://<key-vault-name>.vault.azure.net/ \
        SignClientId=<service-principal-client-id> \
        SignTenantId=<tenant-id> \
        SignKeyVaultCertificate=<certificate-name> \
        SignClientSecret=temporary-value \
      --output table
    ```

    Replace the placeholders with the Key Vault URL, service principal application (client) ID, Microsoft Entra tenant ID, and certificate name. The service principal must have permission to sign with the certificate's key in the Key Vault. Record the group ID returned by this command for the next step.

3. Set the client secret as a protected variable. The CLI prompts for the value so it does not need to be included in the command or shell history:

    ```shell
    az pipelines variable-group variable update \
      --group-id <variable-group-id> \
      --name SignClientSecret \
      --secret true \
      --prompt-value true
    ```

4. In Azure DevOps, open **Pipelines > Library > Sign Client Credentials > Pipeline permissions**. Authorize the Coverlet nightly pipeline specifically. Do not grant open access to all pipelines because the group contains a client secret.

5. Confirm that the group contains `SignKeyVaultUrl`, `SignClientId`, `SignTenantId`, `SignKeyVaultCertificate`, and `SignClientSecret`. The last variable must be marked secret. These names must match the references in `eng/azure-pipelines-nightly.yml`.

## How to manually release packages to nuget.org

This is the steps to release new packages to nuget.org

1. Update projects version files. There are two `version.json` files in the repo. `<roo>\version.json` and `<root>\src\legacy\version.json` (remove `-preview.{height}` and adjust version)

    Do a PR and merge to master.

1. Clone repo, **remember to build packages from master and not from your fork or metadata links will point to your forked repo.** . Run `git log -5` from repo root to verify last commit.

1. From new cloned, aligned and versions updated repo root run build command

  ```shell
  dotnet pack -c release /p:TF_BUILD=true /p:PublicRelease=true
  ...
  coverlet.core net8.0 succeeded (0,3s) → artifacts\bin\coverlet.core\release_net8.0\coverlet.core.dll
  coverlet.core net9.0 succeeded (0,3s) → artifacts\bin\coverlet.core\release_net9.0\coverlet.core.dll
  coverlet.core net10.0 succeeded (0,3s) → artifacts\bin\coverlet.core\release_net10.0\coverlet.core.dll
  coverlet.msbuild.tasks net9.0 succeeded (0,3s) → artifacts\bin\coverlet.msbuild.tasks\release_net9.0\coverlet.msbuild.tasks.dll
  coverlet.MTP net8.0 succeeded (0,3s) → artifacts\bin\coverlet.MTP\release_net8.0\coverlet.MTP.dll
  coverlet.console net10.0 succeeded (0,3s) → artifacts\bin\coverlet.console\release_net10.0\coverlet.console.dll
  coverlet.core netstandard2.0 succeeded (0,1s) → artifacts\bin\coverlet.core\release_netstandard2.0\coverlet.core.dll
  coverlet.msbuild.tasks net10.0 succeeded (0,5s) → artifacts\bin\coverlet.msbuild.tasks\release_net10.0\coverlet.msbuild.tasks.dll
  coverlet.collector net10.0 succeeded (0,5s) → artifacts\bin\coverlet.collector\release_net10.0\coverlet.collector.dll
  coverlet.MTP net10.0 succeeded (0,5s) → artifacts\bin\coverlet.MTP\release_net10.0\coverlet.MTP.dll
  coverlet.collector net9.0 succeeded (0,6s) → artifacts\bin\coverlet.collector\release_net9.0\coverlet.collector.dll
  coverlet.console net9.0 succeeded (0,3s) → artifacts\bin\coverlet.console\release_net9.0\coverlet.console.dll
  coverlet.console net8.0 succeeded (0,4s) → artifacts\bin\coverlet.console\release_net8.0\coverlet.console.dll
  coverlet.msbuild.tasks net8.0 succeeded (0,5s) → artifacts\bin\coverlet.msbuild.tasks\release_net8.0\coverlet.msbuild.tasks.dll
  coverlet.collector net8.0 succeeded (0,4s) → artifacts\bin\coverlet.collector\release_net8.0\coverlet.collector.dll
  coverlet.console net8.0 succeeded (0,0s) → artifacts\publish\coverlet.console\release_net8.0\
  coverlet.MTP netstandard2.0 succeeded (0,2s) → artifacts\bin\coverlet.MTP\release_netstandard2.0\coverlet.MTP.dll
  coverlet.console net10.0 succeeded (0,0s) → artifacts\publish\coverlet.console\release_net10.0\
  coverlet.msbuild.tasks netstandard2.0 succeeded (0,1s) → artifacts\bin\coverlet.msbuild.tasks\release_netstandard2.0\coverlet.msbuild.tasks.dll
  coverlet.console net9.0 succeeded (0,1s) → artifacts\publish\coverlet.console\release_net9.0\
  ...
  ```

1. Sign nuget packages using sign <https://www.nuget.org/packages/sign>

```powershell
sign code azure-key-vault **/*.nupkg --base-directory [ROOT-DIRECTORY]\artifacts\package\release\ --file-digest sha256 --description Coverlet --description-url https://github.com/coverlet-coverage/coverlet `
 --azure-key-vault-url [KEYVAULT-URL] `
 --azure-key-vault-client-id [CLIENT-ID] `
 --azure-key-vault-tenant-id [TENANT-ID] `
 --azure-key-vault-client-secret [KEYVAULT-SECRET] `
 --azure-key-vault-certificate [CERT-FRIENDLY-NAME]
```

1. Upload *.nupkg files to Nuget.org site. **Check all metadata(url links, deterministic build etc...) before "Submit"**

1. **On your fork**:
    * Align to master
    * Bump version by one (fix part) and re-add `-preview.{height}`
    * Create release on repo <https://github.com/coverlet-coverage/coverlet/releases>
    * Update the [Release Plan](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/ReleasePlan.md)(this document) and [ChangeLog](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/Changelog.md)
    * Do PR and merge
