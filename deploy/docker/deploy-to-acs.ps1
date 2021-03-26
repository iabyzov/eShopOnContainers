Param(
    [parameter(Mandatory=$true)][string]$registry
)

if ([String]::IsNullOrEmpty($registry)) {
    Write-Host "Registry must be set to docker registry to use" -ForegroundColor Red
    exit 1 
}

$services = "identity.api", "basket.api", "catalog.api", "ordering.api", "ordering.backgroundtasks", "payment.api", "webhooks.api", "mobileshoppingagg", "webshoppingagg", "ordering.signalrhub", "webstatus", "webspa", "webmvc", "webhooks.client"

foreach ($svc in $services) {
    Write-Host "Creating tag for eshop/${svc}:linux-latest $registry/eshop/${svc}:linux-latest"
    docker tag eshop/${svc}:linux-latest $registry/eshop/${svc}:linux-latest
    docker push $registry/eshop/${svc}:linux-latest
}