#!/usr/bin/env bash

# usage:
# $ build/publicizer.sh

# requires: mono

publicize()
{
  mono ../AssemblyPublicizer/AssemblyPublicizer.exe -i "$1"
}

pushd build/build-depends

publicize Assembly-CSharp.dll
publicize InControl.dll
publicize 0Harmony.dll

popd
