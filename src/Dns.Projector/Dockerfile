FROM mcr.microsoft.com/dotnet/core/runtime-deps:2.2.4

LABEL maintainer "exira.com <info@exira.com>"

COPY / /app
WORKDIR /app

RUN apt-get update && \
    apt-get install curl jq -y && \
    chmod +x ./init.sh

EXPOSE 2001/tcp
ENV ASPNETCORE_URLS http://*:2001

ENTRYPOINT ["./init.sh"]
