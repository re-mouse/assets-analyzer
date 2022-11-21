# assets-analyzer
Tool that provides graphical analysis of assets space usage, and functionality to delete unused

# Install options

#### By clonning repository

- Clone the project to the Assets folder. Folder path should be ProjectFolder/Assets/assets-analyzer

```bash
git clone https://github.com/re-mouse/assets-analyzer.git
```

- Build on platform that you need




#### By installing unity package 

- Go to [release page](https://github.com/re-mouse/assets-analyzer/releases/tag/0.2.0v)

- Download latest unity package

- Import it in unity



## Instructions for use:

Click Project in the top menu of the unit, select the required tool

![](https://github.com/re-mouse/Image-sources/blob/master/Project%20button.png?raw=true)

## Description of each tool:

Assets dependencies | All assets | Unused Assets
--- | --- | ---
Shows the weight of all the assets and their folders currently used in the build, with a display of the memory occupied | Shows all the assets of the project, and display of the memory occupied | Shows assets that are not used in the build, but are in the project folder and gives you the opportunity to delete them by selecting the necessary ones and clicking the bottom delete button (deletes them with a system call, not through a unit, so be sure you want to permanently delete the selected one)

___

## For what:
I wrote it because of the weight of one of my past projects in 14GB, and it was not possible to understand what was related to the project and what was not, and with the help of it I saw all the dependencies, I was able to sort them, and then deleted about 10GB of assets
> It wroted on node system, code written with flexibility and extensibility in mind, so adding any functionality is welcome

## Examples on [pixel-wars repository](https://github.com/re-mouse/pixel-wars)

All Assets | Assets dependency | Unused assets
--- | --- | ---
![](https://github.com/re-mouse/Image-sources/blob/master/AllAssets.png?raw=true) | ![](https://github.com/re-mouse/Image-sources/blob/master/AssetsDependencies.png?raw=true) | ![](https://github.com/re-mouse/Image-sources/blob/master/UnusedAssets.png?raw=true)
