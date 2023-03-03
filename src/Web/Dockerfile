FROM mcr.microsoft.com/dotnet/aspnet:6.0 

LABEL com.aidenwebb.product="simple-redirects"

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        gosu \
        curl \
    && rm -rf /var/lib/apt/lists/*


WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ./obj/build-output/publish .
COPY "/entrypoint.sh" / 

RUN chmod +x /entrypoint.sh

# HEALTHCHECK CMD curl -f http://localhost/alive || exit 1

ENTRYPOINT ["/entrypoint.sh"]