#!/usr/bin/env bash


sudo docker run --name swen2-postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=SWEN2TourPlanner -p 5432:5432 -d postgres