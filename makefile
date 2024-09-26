apiProjectPath=./SimpleBlog.Api/SimpleBlog.Api.csproj

up:
	docker compose up -d

down:
	docker compose down

reset:
	make down && make up

setup_local_db:
	make up && make run_migrations

reset_local_db:
	make reset && make run_migrations
	
add_migration:
	dotnet ef migrations add "$(name)" --context "BlogDbContext" --startup-project $(apiProjectPath) --project $(apiProjectPath)

remove_migration:
	dotnet ef migrations remove --context "BlogDbContext" --startup-project $(apiProjectPath) --project $(apiProjectPath) --force

run_migrations:
	dotnet ef database update --project $(apiProjectPath) --startup-project $(apiProjectPath) --context BlogDbContext