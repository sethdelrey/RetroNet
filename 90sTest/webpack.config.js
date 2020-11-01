/// <binding BeforeBuild='Run - Development' />
const path = require('path');

var paths = {
    webroot: "./"
};

/*var jsFiles = [
    "jquery",
    "bootstrap",
    "jquery-validation-unobtrusive",
    "jquery-ajax-unobtrusive",
    paths.webroot + "wwwroot/js/site.js",
    paths.webroot + "wwwroot/js/pixel.js"

];

var cssFiles = [
    paths.webroot + "node_modules/bootstrap/dist/css/bootstrap.css",
    paths.webroot + "wwwroot/css/main.css",
    paths.webroot + "wwwroot/css/pixel.css",
    paths.webroot + "wwwroot/css/site.css"
];*/

/*var rules = {
    jqueryRule: {
        test: require.resolve('jquery'),
        loader: 'expose-loader',
        options: {
            exposes: ['$', 'jQuery'],
        },
    },
    cssRule: {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
    },
    fontsRule: {
        test: /.(png|gif|ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
        use: ['file-loader']
    },
    cssToTextRule: {
        test: /\.(less|css)$/,
        use: ['style-loader', MiniCssExtractPlugin.loader, 'css-loader']
    }
};

var javascriptExport = {
    mode: devMode ? 'development' : 'production',
    context: path.resolve(__dirname, './'),
    devtool: 'source-map',
    entry: {
        core: jsFiles
    },
    output: {
        path: path.resolve(__dirname, 'wwwroot/js'),
        filename: 'site.min.js'
    },cmd
    module: {
        rules: [
            rules.cssRule,
            rules.fontsRule,
            rules.jqueryRule
        ]
    },
    optimization: {
        minimizer: [new TerserPlugin({
            sourceMap: true
        })],
        minimize: true
    }
};

var cssExport = {
    mode: devMode ? 'development' : 'production',
    plugins: [
        new MiniCssExtractPlugin({
            filename: '[name].min.css',
            chunkFilename: '[id].css'
        })
    ],
    context: path.resolve(__dirname, './'),
    entry: {
        site: cssFiles
    },
    output: {
        path: path.resolve(__dirname, 'wwwroot/css'),
        filename: '[name].css.min.js'
    },
    module: {
        rules: [
            rules.cssToTextRule,
            rules.fontsRule
        ]
    },
    optimization: {
        minimizer: [
            new OptimizeCssAssetsPlugin({
                cssProcessorOptions: {
                    map: { inline: false },
                    discardComments: { removeAll: false }
                },
                canPrint: true
            })
        ]
    }
};*/

module.exports = {
    entry: {
        main: './wwwroot/js'
    },

    output: {
        publicPath: "/generated/",
        path: path.join(__dirname, '/wwwroot/generated/'),
        filename: 'site.min.js'
    }

};