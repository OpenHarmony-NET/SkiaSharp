FROM arm64v8/alpine:3.9

# Arguments:
#   DOTNET_SDK_VERSION   - the version of dotnet for the Cake script        [ 3.1.412 | * ]
RUN echo "http://mirrors.aliyun.com/alpine/v3.9/main" > /etc/apk/repositories \
    && echo "http://mirrors.aliyun.com/alpine/v3.9/community" >> /etc/apk/repositories

RUN apk add -X http://mirrors.aliyun.com/alpine/v3.16/community -u alpine-keys --allow-untrusted
RUN apk add --no-cache bash curl wget python python3 git build-base icu-dev ninja fontconfig-dev libintl clang glib-dev

# use the specific commit before the tool switched to C++20 which is too new for this old alpine
RUN git clone https://gitee.com/wcky/gn /usr/share/gn \
    && cd /usr/share/gn \
    && git checkout d4be45bb28fbfc16a41a1e02c86137df6815f2dd \
    && python build/gen.py --allow-warning \
    && ninja -C out \
    && /usr/share/gn/out/gn --version

RUN wget -O dotnet.tar.gz https://builds.dotnet.microsoft.com/dotnet/Sdk/5.0.408/dotnet-sdk-5.0.408-linux-musl-arm64.tar.gz \
    && dotnet_sha512='4c7c47e887727aa072fa762e289a7c3135120acd2af1ebd591060476606058a387060d732aeece701b3e0795e0d73ef423284738094e6fe654d87641da1ee347' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -xzf dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && rm dotnet.tar.gz

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT 1

RUN dotnet help

ENV GN_EXE=/usr/share/gn/out/gn NINJA_EXE=ninja

WORKDIR /work