# Docker
up:
	docker-compose -f ./deploy/docker-compose.yaml -p jrapi up -d
down:
	docker-compose -f ./deploy/docker-compose.yaml -p jrapi down
up-infra:
	docker-compose -f ./deploy/docker-compose-infra.yaml -p jrapi up -d
down-infra:
	docker-compose -f ./deploy/docker-compose-infra.yaml -p jrapi down

# Ef Core
ef-add:
	dotnet ef migrations add $(NAME) --project .\src\JrApi.Infrastructure\JrApi.Infrastructure.csproj --startup-project .\src\JrApi.Presentation\JrApi.Presentation.Api.csproj
ef-rm:
	dotnet ef migrations remove --project .\src\JrApi.Infrastructure\JrApi.Infrastructure.csproj --startup-project .\src\JrApi.Presentation\JrApi.Presentation.Api.csproj
ef-update:
	dotnet ef database update --project .\src\JrApi.Infrastructure\JrApi.Infrastructure.csproj --startup-project .\src\JrApi.Presentation\JrApi.Presentation.Api.csproj