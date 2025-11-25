# ===== Development stage =====
FROM node:20-alpine AS dev

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
ENV CHOKIDAR_USEPOLLING=true
ENV CHOKIDAR_INTERVAL=200

EXPOSE 5173
CMD ["npm", "run", "dev", "--", "--host"]


# ===== Production build =====
FROM node:20-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build


# ===== Production server =====
FROM nginx:alpine AS prod

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
