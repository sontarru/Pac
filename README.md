# Pac

The simple web application to serve
[PAC (proxy autoconfiguration)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/Proxy_servers_and_tunneling/Proxy_Auto-Configuration_PAC_file)
files.

## Run in docker container

Create the folder for the PAC file.

```bash
mkdir /path/to/pac -p
```

Create the  PAC file boilerplate.

```bash
cat << EOF | tee /path/to/pac/default.pac
// Add domains to request through the proxy.
const domainsToProxy = [
    'example.com'
].map(dn => dn.toUpperCase());

// Set this to the actual proxy address and port.
const proxy = 'SOCKS 192.168.0.1:1080'

function FindProxyForURL(url, host) {
    host = host.toUpperCase();

    const match = dn =>
        host.toUpperCase().endsWith('.' + dn) ||
        host === dn;

    const useProxy = domainsToProxy.some(match);
    return useProxy ? proxy : 'DIRECT';
}
EOF
```

Log in to the GitHub container registry.

```bash
docker login -u sontarru -p <PAT> ghcr.io
```

Run the container.

```bash
docker run --name pac -v /home/pac:/app/wwwroot \
    -p 8080:8080 -d ghcr.io/sontarru/pac:latest
```

Set up you browser to use `http://192.168.0.1:8080` as a proxy autoconfiguration URL
(change the address and port to the actual ones).
