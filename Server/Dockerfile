# 选择基础镜像
## alpine ##
# node:alpine无法使用npm安装node-7z-archive包，改用node:bullseye
#FROM node:alpine
# 最新版本
#FROM node:bullseye
# 锁版本
FROM node:20-bookworm

## ubuntu ##
# FROM balenalib/amd64-ubuntu-node:latest

# 设置维护者信息
LABEL maintainer="richie"

# 防止中文打印信息显示乱码
ENV LANG="C.UTF-8"
ENV TZ=Asia/Shanghai

# 更换aliyun源
# RUN sed -i 's@deb.debian.org@mirrors.aliyun.com@g' /etc/apt/sources.list.d/debian.sources
# RUN sed -i 's@deb.debian.org@mirrors.aliyun.com@g' /etc/apt/sources.list

# 新软件源并安装
## node:alpine ##
# RUN apk add p7zip zlib tar
# RUN apk add --no-cache build-base gcc g++ make
# RUN apk add p7zip zlib wget tar xz 

#node:bullseye
# 安装 7zip
# RUN echo "deb http://mirrors.aliyun.com/debian wheezy main contrib non-free \
# deb-src http://mirrors.aliyun.com/debian wheezy main contrib non-free \
# deb http://mirrors.aliyun.com/debian wheezy-updates main contrib non-free \
# deb-src http://mirrors.aliyun.com/debian wheezy-updates main contrib non-free \
# deb http://mirrors.aliyun.com/debian-security wheezy/updates main contrib non-free \
# deb-src http://mirrors.aliyun.com/debian-security wheezy/updates main contrib non-free" > /etc/apt/sources.list
# RUN apt-get update && \
#     apt-get install -y p7zip-full && \
#     rm -rf /var/lib/apt/lists/*

##ubuntu
# RUN apt-get update && apt-get install -y p7zip-full

# 安装 canvas 编译所需的依赖
RUN apt-get update && \
    apt-get install -y \
    build-essential \
    python3 \
    pkg-config \
    libpango1.0-dev \
    libcairo2-dev \
    libatomic1 \
    libjpeg-dev \
    libgif-dev && \
    rm -rf /var/lib/apt/lists/*


# WORKDIR：设置工作目录，所有后续指令（如RUN、COPY、ADD）都会在这个目录下执行。如果目录不存在，会自动创建。
WORKDIR /app

# COPY <源路径> <目标路径> 将文件或目录从构建上下文复制到镜像中。（如有忽略文件配置在.dockerignore中）
COPY ./package.json /app
COPY ./*.mjs /app
#COPY ./mir2_resources.zip /app
#OPY ./mir2_resources.snda /app
#COPY ./mir2_resources.wemade /app


#解压
#RUN unzip mir2_resources.zip
#RUN rm -rf ./mir2_resources.zip

# 配置国内镜像源
RUN npm config set registry https://registry.npmmirror.com/

# 安装依赖并重建
RUN rm -rf ./node_modules package-lock.json
#RUN npm cache clean --force
#RUN npm install --verbose
RUN yarn install
RUN yarn --verbose

# 创建符号链接以解决路径问题
# RUN mkdir -p /app/node_modules/node-7z-archive/binaries/linux \    && ln -s /usr/bin/7za /app/node_modules/node-7z-archive/binaries/linux/7za

# EXPOSE: 声明镜像要监听的端口，这只是一个文档化的指令，实际上并不会开放端口。
EXPOSE 5180

# CMD：指定容器启动时要执行的命令，Dockerfile中只能有一个CMD指令，后面的会覆盖前面的
CMD ["node", "app.mjs"]
