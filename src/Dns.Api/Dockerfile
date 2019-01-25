FROM microsoft/dotnet:2.2.0-runtime-deps

LABEL maintainer "exira.com <info@exira.com>"

COPY / /app
WORKDIR /app

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["./Dns.Api"]
