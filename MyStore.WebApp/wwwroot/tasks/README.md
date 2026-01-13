# Build Tasks - Các script tự động hóa

## Hướng dẫn sử dụng

### Compile SCSS
```bash
sass scss/main.scss css/main.css --watch
```

### Minify CSS
```bash
npx csso css/main.css --output css/main.min.css
```

### Minify JavaScript
```bash
npx terser js/site.js --output js/site.min.js
```

## NPM Scripts (thêm vào package.json)
```json
{
  "scripts": {
    "scss": "sass scss/main.scss css/main.css",
    "scss:watch": "sass scss/main.scss css/main.css --watch",
    "build": "npm run scss && npm run minify",
    "minify": "csso css/main.css -o css/main.min.css"
  }
}
```
