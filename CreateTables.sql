CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "Category" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(255) NOT NULL
);

CREATE TABLE "Product" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(255) NOT NULL,
    "Price" DECIMAL(10, 2) NOT NULL,
    "CategoryId" UUID ,
    "IsActive" BOOLEAN NOT NULL,
    "DateCreated" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DateUpdated" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "CreatedByUserId" UUID,
    "UpdatedByUserId" UUID,
    CONSTRAINT "FK_Product_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id")
);

CREATE TABLE "Review" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Description" VARCHAR(255) NOT NULL,
    "Rating" INT,
    "IsActive" BOOLEAN NOT NULL,
    "DateCreated" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DateUpdated" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "CreatedByUserId" UUID,
    "UpdatedByUserId" UUID
);

