for /f %%i in (dir.txt) do type %%i >> upload.js

java -jar yuicompressor.jar  --type js --charset utf-8  upload.js -o upload-min.js 
