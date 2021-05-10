#!/usr/bin/env bash

# usage:
# $ build/build.sh

# requires: nuget, mono-complete, monodevelop

pushd src

nuget restore

mdtool build -c:Testing -t:Clean
mdtool build -c:Testing -t:Build

mdtool build -c:Testing -t:Clean
mdtool build -c:Release -t:Build

popd
