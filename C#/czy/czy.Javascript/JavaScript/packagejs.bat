for /f %%i in (dir.txt) do type %%i >> core.js

java -jar yuicompressor.jar  --type js --charset utf-8  core.js -o core-min.js 
