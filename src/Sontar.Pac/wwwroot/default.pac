// Add domains to request through the proxy.
const domainsToProxy = [
    'myip.com'
].map(dn => dn.toUpperCase());

// Set this to the actual proxy.
const proxy = 'SOCKS 10.42.0.1:1080'

const FindProxyForURL = (url, host) => {
    host = host.toUpperCase();

    const match = dn =>
        host.toUpperCase().endsWith('.' + dn) ||
        host === dn;

    const useProxy = domainsToProxy.some(match);
    return useProxy ? proxy : 'DIRECT';
}
