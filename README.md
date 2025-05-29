# AssetIO

## Description
AssetIO is a lightweight portable application used to track assets as they enter and leave inventory.
It is meant to be designed for the simplest possible interaction when a user needs to add or remove an item from inventory without opening and logging in to a larger inventory management system.
It is NOT meant as a replacement for an inventory tracking system, as the app does not store any data about an item, nor does it know if a specific item is in inventory or not.

## Installation
Download the desired release and unzip the contents.
Launch the executable. If this is the first time you are launching it will take a few moments to compile.

## Usage
### First Time Setup
1. The first time you launch the application (or if you delete the config file), you will be prompted to select a database path in settings.
2. Click AssetIO > Settings, then select a folder using the file selector. This will be where the data is saved.
3. Creating new databases is as simple as creating a new folder and selecting it in settings. This way multiple inventories can be updated.

### Standard Usage
1. The user that is updating the inventory will enter their name.
2. They will enter (or scan) the desired identifier of the asset into the correct box - Devices In for receiving devices, Devices Out for devices leaving.
3. Click Clear to clear the input boxes, or Submit to save the record.
4. The devices will appear on the right.
![image](https://github.com/user-attachments/assets/07abd97c-2fed-4655-8be2-70689c095fb6)

## Viewing past entries
1. To view past entries, click AssetIO > Open Log Viewer.
2. Select the desired folder using the file selector. This folder will be scanned for .aio files.
3. The window will display the number of .aio files found in that directory. If it is 0, that's likely the wrong folder.
![image](https://github.com/user-attachments/assets/1ab9ef63-d197-4b3e-97cd-7f5b3c47b26a)
5. Click Submit, and a window will open displaying the contents of all of the .aio files found.
![image](https://github.com/user-attachments/assets/78047c91-92f7-41c8-b684-8e7f8aae0db2)

## How data is stored
Afer you select a database path, the app will save .aio files to that directory.
.aio files are simply comma separated text files.
Saving occurs when the user clicks the Submit button.
A new file will be created if one does not already exist for that date.

## To-do
- Update logic for saving - Currently if data is saved and then the app is left running overnight, and then new data is saved, the new file for that day will contain the items from the previous day as well, resulting in duplicate entries.
- Add ability to search logs for specific items
- Add ability to display logs only between given dates
