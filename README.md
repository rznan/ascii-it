A simple tool to make ASCII Art from an image.


## How to use:


```pwsh
dotnet run -- -i "targetImage.jpg" 
<# This will generate an ascii art image based on the target image.#>
```
OR

```pwsh
dotnet run -- -i "targetImage.jpg" -p
<#This will print an ascii art to the terminal base on the targe image.#>
```


Options:
```
-i, --input     Required. Path to the input image
-o, --output    Path to the desired output
-p, --print     To print the result to the terminal
--help          Display the help screen.
--version       Display version information.
```


## Limitations
### Works only on windows;