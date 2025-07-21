# ECS Learning Enviroment - Development Project
This repository contains the development environment for an experimental integration of Unity's ML-Agents with DOTS

> [!WARNING]  
> This is an active, unfinished project. Expect significant technical debt and architectural issues. This repository serves as a public prototyping space, so curious eyes are welcome!

## How to Test
This project uses the official Unity ML-Agents package. You must install `mlagents` via `pip`. It is highly recommended to do this in a virtual environment 

Follow the official installation guide [ML Agents - Installation](https://github.com/Unity-Technologies/ml-agents/blob/release_22_docs/docs/Installation.md)
## `<Commits>`
* **build**: Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm)
* **ci**: Changes to our CI configuration files and scripts (example scopes: Circle, BrowserStack, SauceLabs)
* **docs**: Documentation only changes
* **feat**: A new feature
* **license**: Licensing compliance and changes
* **fix**: A bug fix
* **perf**: A code change that improves performance
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
* **test**: Adding missing tests or correcting existing tests
* **revert**: If the commit reverts a previous commit, it should begin with `revert: `, followed by the header of the reverted commit. In the body it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.