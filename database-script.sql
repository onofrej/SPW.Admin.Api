DROP TABLE IF EXISTS "user";
DROP TABLE IF EXISTS "special_day";
DROP TABLE IF EXISTS "congregation";
DROP TABLE IF EXISTS "circuit";
DROP TABLE IF EXISTS "validity";
DROP TABLE IF EXISTS "schedule";
DROP TABLE IF EXISTS "point";
DROP TABLE IF EXISTS "announcement";
DROP TABLE IF EXISTS "holiday";
DROP TABLE IF EXISTS "domain";

CREATE TABLE "domain" (
	"id" UUID PRIMARY KEY,
	"name" VARCHAR
);

CREATE TABLE "validity" (
    "id" UUID PRIMARY KEY,
    "start_date" TIMESTAMP NOT NULL,
    "end_date" TIMESTAMP NOT NULL,
    "status" BOOLEAN NOT null,
    "domain_id" UUID NOT null,
	FOREIGN KEY (domain_id) REFERENCES "domain"(id)
);

CREATE TABLE "circuit" (
	"id" UUID PRIMARY KEY,
	"name" VARCHAR,
	"domain_id" UUID NOT null,
	FOREIGN KEY (domain_id) REFERENCES "domain"(id)
);

CREATE TABLE "congregation" (
	"id" UUID PRIMARY KEY,
	"name" VARCHAR,
	"number" VARCHAR,
	"circuit_id" UUID NOT null,
	FOREIGN KEY (circuit_id) REFERENCES circuit(id)
);

CREATE TABLE "user" (
    "id" UUID PRIMARY KEY,
    "name" VARCHAR,
    "creation_date" TIMESTAMP,
    "email" VARCHAR,
    "phone_number" VARCHAR,
    "gender" VARCHAR,
    "birth_date" DATE,
    "baptism_date" DATE,
    "privilege" VARCHAR,
    "congregation_id" UUID NOT null,
	FOREIGN KEY (congregation_id) REFERENCES "congregation"(id)
);

CREATE TABLE "special_day" (
	"id" UUID PRIMARY KEY,
	"name" VARCHAR,
	"start_date" TIMESTAMP NOT NULL,
	"end_date" TIMESTAMP NOT NULL,
	"circuit_id" UUID NOT null,
	FOREIGN KEY (circuit_id) REFERENCES circuit(id)
);

CREATE TABLE "schedule" (
	"id" UUID PRIMARY KEY,
	"time" VARCHAR,
	"domain_id" UUID NOT null,
	FOREIGN KEY (domain_id) REFERENCES "domain"(id)
);

CREATE TABLE "point" (
    "id" UUID PRIMARY KEY,
    "name" VARCHAR,
    "number_of_publishers" INTEGER NOT NULL,
    "address" VARCHAR,
    "image_url" VARCHAR,
    "google_maps_url" VARCHAR,
    "domain_id" UUID NOT null,
	FOREIGN KEY (domain_id) REFERENCES "domain"(id)
);

CREATE TABLE "announcement"(
    "id" UUID PRIMARY KEY,
    "title" VARCHAR,
    "message" text,
    "domain_id" UUID NOT null,
	FOREIGN KEY (domain_id) REFERENCES "domain"(id)
);

CREATE TABLE "holiday" (
    "id" UUID PRIMARY KEY,
    "name" VARCHAR,
    "date" DATE NOT null,
    "domain_id" UUID NOT null,
	FOREIGN KEY (domain_id) REFERENCES "domain"(id)
);
