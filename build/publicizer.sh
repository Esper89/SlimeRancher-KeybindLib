#!/bin/bash

publicize()
{
  mono ../AssemblyPublicizer/AssemblyPublicizer.exe -i "$1"
}

pushd build/build-depends

publicize Assembly-CSharp.dll
publicize SRML.dll
publicize InControl.dll

popd
