[CmdletBinding()]
param(
  [Parameter(ValueFromPipeline,
    ValueFromPipelineByPropertyName,
    Position = 1,
    ParameterSetName = "Default",
    HelpMessage = "The tag to tag the built image with.")]
  [string]$Tag = "{{.Run.ID}}"
)

process {
  az acr build -t "mocshortlinkmakerbot:$Tag" -f ./Dockerfile ./ --registry iamtheantitwink
}
