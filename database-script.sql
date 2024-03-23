CREATE TABLE "user" (
    "id" UUID PRIMARY KEY,
    "name" VARCHAR,
    "creation_date" TIMESTAMP,
    "email" VARCHAR,
    "phonenumber" VARCHAR,
    "gender" VARCHAR,
    "birthdate" DATE,
    "baptismdate" DATE,
    "privilege" VARCHAR
);