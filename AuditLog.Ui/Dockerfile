# Use node for building
FROM node:18 AS build
WORKDIR /app
COPY . .
RUN npm install && npm run build

# Use nginx for serving the built files
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]

