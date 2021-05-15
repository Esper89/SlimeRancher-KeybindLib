# Targets:
#	all				Test and build the mod. Default target.
#	clean			Remove all traces of previous builds.
#	build			Just build the mod.
#	test			Build the mod and the example.
#	debug			Build the mod and example in debug mode.
#	install			Build the mod and install it.
#	uninstall		Remove the installed mod.
#	dist			Build the mod and zip the output.

SHELL = /bin/sh

NAME = KeybindLib
BUILD = mdtool build src/$(NAME).sln
TARGET = build/target/$(NAME).dll

.PHONY: all clean build test install uninstall

all: test build

clean:
	$(BUILD) -c:Debug -t:Clean
	$(BUILD) -c:Testing -t:Clean
	$(BUILD) -c:Release -t:Clean

build:
	$(BUILD) -c:Release

test:
	$(BUILD) -c:Testing -t:Clean
	$(BUILD) -c:Testing

debug:
	$(BUILD) -c:Debug

install: build
	cp -t "$(shell cat "build/mods-dir")" $(TARGET)

uninstall:
	rm -f -- "$(shell cat "build/mods-dir")/$(NAME).dll" $(TARGET)

dist: build
	zip -r build-target.zip build/target/*
