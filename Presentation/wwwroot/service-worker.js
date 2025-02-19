// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });

//self.addEventListener('install', (event) => {
//    event.waitUntil(
//        caches.open(CACHE_NAME).then((cache) => {
//            return cache.addAll([
//                '/LinearAlgebra/index.html',
//                '/LinearAlgebra/service-worker.js',
//                // other assets...
//            ]);
//        })
//    );
//});