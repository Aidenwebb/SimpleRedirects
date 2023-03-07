#!/bin/bash

# Setup

GROUPNAME="simpleredirects"
USERNAME="simpleredirects"

LUID=${LOCAL_UID:-0}
LGID=${LOCAL_GID:-0}

# Set down from host root to well-known nobody/nogroup user

if [ $LUID -eq 0 ]; then
    LUID=65534
fi
if [ $LGID -eq 0 ]; then
    LGID=65534
fi

# Create user and group

groupadd -o -g $LGID $GROUPNAME >/dev/null 2>&1 ||
groupmod -o -g $LGID $GROUPNAME >/dev/null 2>&1
useradd -o -u $LUID -g $GROUPNAME -s /bin/false $USERNAME >/dev/null 2>&1 ||
usermod -o -u $LUID -g $GROUPNAME -s /bin/false $USERNAME >/dev/null 2>&1
mkhomedir_helper $USERNAME

# The rest...

chown -R $USERNAME:$GROUPNAME /app
mkdir -p /etc/simpleredirects/core
mkdir -p /etc/simpleredirects/logs
mkdir -p /etc/simpleredirects/ca-certificates
chown -p $USERNAME/$GROUPNAME /etc/simpleredirects

cp /etc/simpleredirects/ca-certificates/* /usr/local/share/ca-certificates/ >/dev/null 2>&1 \
&& update-ca-certificates

exec gosu $USERNAME:$GROUPNAME dotnet /app/Web.dll