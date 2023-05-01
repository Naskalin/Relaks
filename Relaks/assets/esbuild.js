import esbuild from "esbuild";
import { sassPlugin } from "esbuild-sass-plugin";
import postcss from 'postcss';
import autoprefixer from 'autoprefixer';

// Generate CSS/JS Builds
const context = await esbuild.context({
    entryPoints: [
        "src/js/libs.js",
        "src/js/app.js",
    ],
    outdir: "../wwwroot/assets",
    bundle: true,
    metafile: false,
    sourcemap: false,
    minify: true, 
    loader: {
        ".png": "dataurl",
        ".woff": "dataurl",
        ".woff2": "dataurl",
        ".eot": "dataurl",
        ".ttf": "dataurl",
        ".svg": "dataurl",
    },
    plugins: [
        sassPlugin({
            async transform(source) {
                const { css } = await postcss([autoprefixer]).process(source, {
                    map: false,
                    from: undefined,
                });
                return css;
            },
        }),
    ],
})
;

// Manually do an incremental build
const result = await context.rebuild().then(() => {
    console.log("⚡ Build complete! ⚡");
})

// Enable watch mode
await context.watch().then(() => {
    console.log("⚡ Watching...");
})