const path = require('path')
const PrerenderSPAPlugin = require('prerender-spa-plugin')
const Renderer = PrerenderSPAPlugin.PuppeteerRenderer

module.exports = {
 configureWebpack: config => {
   if (process.env.NODE_ENV !== 'production') return

   return {
     plugins: [
       new PrerenderSPAPlugin({
         // Absolute path to compiled SPA
         staticDir: path.resolve(__dirname, 'dist'),

         renderer: new Renderer({
           consoleHandler: function (route, msg) {
             for (let i = 0; i < msg.args().length; ++i)
               console.log(`${i}: ${msg.args()[i]}`);
           },
           renderAfterDocumentEvent: 'prerender-page-ready',
           // tell the app we are prerendering
           injectProperty: '__PRERENDER_INJECTED',
           inject: {
             foo: 'bar' // required for that to work
           }
         }),
         // List of routes to prerender
         routes: ['/', '/clusters'],
         postProcess(renderedRoute) {
           // Remove /index.html from the output path if the dir name ends with a .html file extension.
           // For example: /dist/dir/special.html/index.html -> /dist/dir/special.html
           if (!renderedRoute.route.endsWith('/')) {
             renderedRoute.outputPath = path.join(__dirname, 'dist', renderedRoute.route + ".html")
           }

           return renderedRoute
         }
       })
     ]
   }
 }
}
