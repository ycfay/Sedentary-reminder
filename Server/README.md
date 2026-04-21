
##### build image
```shell
rm -rf yarn.lock  package-lock.json  README.md node_modules MapInfo.txt
docker stop image-server && docker rm image-server
docker rmi yc/image-node-app
docker build -t yc/image-node-app:latest .
```

##### run container
```shell
docker run -it --rm  --name image-server yc/mir2-node-app:latest

## snda
docker run -d \
    --name image-server \
    -p 5180:5180 \
    -v /root/assets:/app/assets \
    -v /etc/localtime:/etc/localtime:ro \
    yc/image-node-app:latest


##### access the container
```shell
docker exec -it mir2-server /bin/sh
```


##### update files
```sh
docker cp app.mjs image-server:/app
```


##### LOCAL RUN
```bat
node .\app.mjs
```

