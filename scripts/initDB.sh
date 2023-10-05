#!/usr/bin/env bash
sudo apt install sqlite
sqlite3 ../data/chirp.db < ../data/schema.sql
sqlite3 ../data/chirp.db < ../data/dump.sql