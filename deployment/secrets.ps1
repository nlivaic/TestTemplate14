param ($keyVaultName, $dbConnectionString, $messageBrokerConnectionString, $authAuthority, $authAudience, $authValidIssuer)

# Will expose method NewPassword
. $PSScriptRoot\secretGenerator.ps1

################################################################
### Set Key Vault secrets to provided values.
################################################################
$sqlSaPasswordSecretName = "SqlSaPassword"
$sqlAdminPasswordSecretName = "SqlAdminPassword"
$dbConnectionName = 'TestTemplate14DbConnection'
$messageBrokerName = 'MessageBroker'
$applicationInsightsConnectionName = 'ApplicationInsightsConnectionString'
$auth_authority_env_var_name = 'AuthAuthority'
$auth_audience_env_var_name = 'AuthAudience'
$auth_valid_issuer_env_var_name = 'AuthValidIssuer'

# We have to check whether all the relevant secrets are in there.
# If not, generate those secrets and store in Key Vault.
$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($sqlSaPasswordSecretName)')"
$exists = az keyvault secret list --vault-name $keyVaultName --query $query
if ($exists -eq "false") {
	az keyvault secret set --vault-name $keyVaultName --name $sqlSaPasswordSecretName --value $(NewPassword) --output none
	Write-Host "##[section]Set secret $sqlSaPasswordSecretName"
}

$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($sqlAdminPasswordSecretName)')"
$exists = az keyvault secret list --vault-name $keyVaultName --query $query
if ($exists -eq "false") {
	az keyvault secret set --vault-name $keyVaultName --name $sqlAdminPasswordSecretName --value $(NewPassword) --output none
	Write-Host "##[section]Set secret $sqlAdminPasswordSecretName"
}

$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($auth_authority_env_var_name)')"
$exists = az keyvault secret list --vault-name $keyVaultName --query $query
if ($exists -eq "false") {
	az keyvault secret set --vault-name $keyVaultName --name $auth_authority_env_var_name --value "$($authAuthority)" --output none
	Write-Host "##[section]Set secret $auth_authority_env_var_name"
}

$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($auth_audience_env_var_name)')"
$exists = az keyvault secret list --vault-name $keyVaultName --query $query
if ($exists -eq "false") {
	az keyvault secret set --vault-name $keyVaultName --name $auth_audience_env_var_name --value "$($authAudience)" --output none
	Write-Host "##[section]Set secret $auth_audience_env_var_name"
}

$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($auth_valid_issuer_env_var_name)')"
$exists = az keyvault secret list --vault-name $keyVaultName --query $query
if ($exists -eq "false") {
	az keyvault secret set --vault-name $keyVaultName --name $auth_valid_issuer_env_var_name --value "$($authValidIssuer)" --output none
	Write-Host "##[section]Set secret $auth_valid_issuer_env_var_name"
}

################################################################
### Set connection strings.
################################################################
if ($dbConnectionString -ne $null) {
	$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($dbConnectionName)')"
	$exists = az keyvault secret list --vault-name $keyVaultName --query $query
	if ($exists -eq "false") {
		az keyvault secret set --vault-name $keyVaultName --name $dbConnectionName --value "$($dbConnectionString)" --output none
		Write-Host "##[section]Set secret $dbConnectionName"
	}
}

if ($messageBrokerConnectionString -ne $null) {
	$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($messageBrokerName)')"
	$exists = az keyvault secret list --vault-name $keyVaultName --query $query
	if ($exists -eq "false") {
		az keyvault secret set --vault-name $keyVaultName --name $messageBrokerName --value "$($messageBrokerConnectionString)" --output none
		Write-Host "##[section]Set secret $messageBrokerName"
	}
}
