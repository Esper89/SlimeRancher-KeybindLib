#!/usr/bin/env bash

# usage:
# $ build/autosetup.sh [SLIMERANCHER INSTALL PATH]

# requires mono

cp "$*"/SlimeRancher_Data/Managed/* build/build-depends
cp "$*"/SRML/Libs/* build/build-depends
echo "$*"/SRML/Mods > build/mods-dir
build/publicizer.sh
