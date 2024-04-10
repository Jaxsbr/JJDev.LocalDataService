# Local Data Service (LDS) | Object Persistence

## Overview

The Local Data Service (LDS) library facilitates storing code objects as data files on the local computer. It abstracts away IO logic, providing engineers with a quick and reliable method for storing and loading data structures without concerns about serialization, file, and directory validation.

## Usecases

Developers of business applications can utilize this library to convert C# classes/objects into JSON format and save them to a local JSON file. Subsequently, they can read these local JSON files and convert them back to the original C# class/object.

## Problem 1

When saving different objects to file, it becomes challenging to determine which files need to be loaded into memory based solely on their file name. Typically, all files must be loaded into memory in order to gain access to their content and ascertain details such as the object owner (user) or the groups to which the object needs to be categorized.

This challenge poses issues of performance and efficiency. Unlike in data management technologies such as SQL, where various mechanisms are employed to efficiently index, page, and filter data, a custom file storage system lacks these capabilities. Consequently, there is a need to develop a mechanism from scratch to avoid costly IO operations resulting from unnecessary file loading.

## Solution 1

The data we are managing with this library needs to transition between two states:

1. `Object state`, the code object/class the resides in memory
2. `File state`, the JSON representation of an object/class stored on a local computer file.

Solution one addresses the logic flow from `File state` to `Object state`, specifically which files need to transition.

For example:
In a veterinary application that manages clients pets, such as dogs, cats and reptiles. Each of these entities will be represented by different C# classes and will have different properties. The list of pets might be very large and consequently we'll have many local files, one per pet.
When the application needs to load all dog files, how would it know which of the files are dogs and which are cats or reptiles.

Previously I've solved this solution in two different ways.

1. `File name formatting`. Set the file's name to a specific format. The application interpret the file name and gains insight without having to load the file into memory. e.g. `TYPE-DOG-{fileid}.json`. The application starts reading after `TYPE-` and stops after the second `-`. The content in between will be `DOG` and will be loaded.
2. `Custom manifest files`. Utilize a manifest file that resides with the stored entity files. The manifest is aware of each entity file, it's name and the type of content it contains. e.g. Manifest content

| Type Header     | Name Header     | Status Header   |
|-----------------|-----------------|-----------------|
| Dog             | 123.json        | Active          |
| Cat             | 456.json        | Archived        |

```json
[
    {
        "Type Header": "Dog",
        "Name Header": "123.json",
        "Status Header": "Active"
    },
    {
        "Type Header": "Cat",
        "Name Header": "456.json",
        "Status Header": "Archived"
    }
]
```

Both options have pros and cons, however, the manifest approach provides more flexibility. The manifest can track many "meta" fields about a file, versus the file name formatting approach which is limited to the max length of a file.
