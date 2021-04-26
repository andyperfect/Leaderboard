# Leaderboard

The aim of this project is to create an opensource backend and frontend solution for public leaderboards. The primary target is for speedrunning leaderboards, but it is built in a way that should allow for its use in nearly any capacity.

## Getting started

Currently, the backend uses a sqlite database. To create it, run the API project and make the following request, with credentials for a user that will be an administrator for the site

    curl --location --request POST 'http://localhost:5000/Database/Initialize' \
    --header 'Content-Type: application/json' \
    --data-raw '{
        "email": "ap@something.com",
        "password": "uniquepassword",
        "username": "ap"
    }'

