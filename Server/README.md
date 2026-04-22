
##### build image
```shell
rm -rf yarn.lock  package-lock.json  README.md node_modules MapInfo.txt
docker stop image-server && docker rm image-server
docker rmi image-app
docker build -t image-app:latest .
```

##### run container
```shell
docker run -it --rm  --name image-server image-app:latest

## snda
docker run -d \
    --name image-server \
    -p 5180:5180 \
    -v /root/assets:/app/assets \
    -v /etc/localtime:/etc/localtime:ro \
    image-app:latest

docker inspect e4020c91b0da

##### access the container
```shell
docker inspect e4020c91b0da
docker exec -it mir2-server /bin/sh
ILmerge /ndebug /target:winexe /out:newApp.exe /log F:\t_code\co\dm23\Sedentary-reminder\Reminder\bin\Debug\Reminder.exe F:\t_code\co\dm23\Sedentary-reminder\Reminder\bin\Debug\Magick.NET-Q16-AnyCPU.dll F:\t_code\co\dm23\Sedentary-reminder\Reminder\bin\Debug\Magick.NET.Core.dll
```


##### update files
```sh
docker cp app.mjs image-server:/app
```


##### LOCAL RUN
```bat
node .\app.mjs
```

