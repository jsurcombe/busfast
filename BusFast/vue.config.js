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
       })
     ]
   }
 }
}
