$deploymentFolder = (Get-Item $PSScriptRoot).Parent
$buildImages = $true
$version = "v1"

$TotalTime = New-Object -TypeName System.Diagnostics.Stopwatch
$TotalTime.Start()

Write-Host "Deploying infrastructure services"
kubectl apply -f (Join-Path $deploymentFolder k8s_infra_manifest.yaml)

if ($buildImages)
{
    ## tag and push containers 
    $images = "musicservice", "orderservice", "shoppingcartservice", "musicstore"
    foreach ($image in $images)
    {
        $tag = $image + ":" + $version
        docker-compose build --parallel $image
        Write-Host "Tagging $image with $tag"
        docker tag $image $tag
    }
}

kubectl apply -f (Join-Path $PSScriptRoot musicconfig.yaml)

Write-Host "Replacing tokens in app manifest with env-specific values"
((Get-Content -Path (Join-Path $deploymentFolder k8s_template_apps.yaml) -Raw) `
    -replace "LoadBalancer", "NodePort" `
    -replace '<acr>/', "" `
    -replace '<version>', $version) | `
    Set-Content -Path (Join-Path $PSScriptRoot local_apps_manifest.yaml)

Write-Host "Deploying Apps"
kubectl apply -f (Join-Path $PSScriptRoot local_apps_manifest.yaml)

$TotalTime.Stop()
Write-Host "Total processing time:" $TotalTime.Elapsed.ToString()