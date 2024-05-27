-- CreateTable
CREATE TABLE "Pizzas" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "calories" INTEGER NOT NULL,
    "description" TEXT
);

-- CreateTable
CREATE TABLE "Weapons" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "dps" INTEGER NOT NULL
);

-- CreateTable
CREATE TABLE "Turtles" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "weaponId" INTEGER NOT NULL,
    "favoritePizzaId" INTEGER NOT NULL,
    "color" TEXT NOT NULL,
    "image" TEXT NOT NULL,
    CONSTRAINT "Turtles_weaponId_fkey" FOREIGN KEY ("weaponId") REFERENCES "Weapons" ("id") ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT "Turtles_favoritePizzaId_fkey" FOREIGN KEY ("favoritePizzaId") REFERENCES "Pizzas" ("id") ON DELETE RESTRICT ON UPDATE CASCADE
);

-- CreateIndex
CREATE UNIQUE INDEX "Pizzas_name_key" ON "Pizzas"("name");

-- CreateIndex
CREATE UNIQUE INDEX "Weapons_name_key" ON "Weapons"("name");

-- CreateIndex
CREATE UNIQUE INDEX "Turtles_name_key" ON "Turtles"("name");
