up:
	docker compose up -d

down:
	docker compose down

reset:
	make down && make up