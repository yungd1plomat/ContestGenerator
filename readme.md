# ContestGenerator

ContestGenerator — это платформа для создания и проведения олимпиад.

# Содержание
- Установка и развертывание платформы
  - [Предварительные требования]((#предварительные-требования))
  - [Конфигурация](#конфигурация)
  - [Развертывание платформы](#развертывание-платформы)
- Работа с платформой
  - [Доступ к платформе](#доступ-к-платформе)
  - [Управление пользователями](#управление-пользователями)
  - [Загрузка файлов](#загрузка-файлов)
  - [Создание конкурса](#создание-конкурса)
  - [Привязка домена](#привязка-домена)

## Установка и развертывание платформы

### Предварительные требования
- Сервер с установленным Docker и IPV4 адресом
- Docker версии 20.10 или выше
- Домен

### Конфигурация
#### docker-compose.yml
В файле [docker-compose.yml](https://github.com/yungd1plomat/ContestGenerator/blob/master/docker-compose.yml) определены следующие переменные окружения для различных сервисов:

Сервис pg (PostgreSQL):
- POSTGRES_USER: имя пользователя для подключения к базе данных.
- POSTGRES_PASSWORD: пароль пользователя для подключения к базе данных.
- POSTGRES_DB: имя создаваемой базы данных.

Сервис contestgenerator:
- CONNECTION_STRING: строка подключения к базе данных PostgreSQL, содержащая хост, порт, имя базы данных, имя пользователя и пароль.
- ASPNETCORE_ENVIRONMENT: определяет среду выполнения приложения (Development, Staging, Production).
- ASPNETCORE_URLS: URL, на котором приложение будет принимать запросы.
- ADMIN_PASS: начальный пароль администратора для доступа к платформе.

#### Caddyfile
Файл [Caddyfile](https://github.com/yungd1plomat/ContestGenerator/blob/master/Caddy/Caddyfile) содержит начальную конфигурацию Caddy. 

Ниже приведена конфигурация для домена https://mydomain.com.
Замените mydomain.com на домен по которому будет доступна платформа.
```
{
  admin       :2019
  auto_https off
}

mydomain.com {
  reverse_proxy {
    to contestgenerator:5000
    header_up -X-Forwarded-Host
  }
}
```

### Развертывание платформы
1. Создайте [A-запись](https://help.reg.ru/support/dns-servery-i-nastroyka-zony/nastroyka-resursnykh-zapisey-dns/nastroyka-resursnykh-zapisey-v-lichnom-kabinete#1) в настройках DNS вашего домена, указывающую на IP-адрес сервера, где будет развернута платформа
2. Клонируйте репозиторий
```bash
git clone https://github.com/yungd1plomat/ContestGenerator.git
cd ContestGenerator
```
3. Измените параметры конфигурации в файлах Caddyfile и docker-compose.yml
4. Создайте сеть `caddy` командой
```bash
docker network create caddy
```
5. Запустите контейнеры командой
```bash
docker-compose up -d
```
6. Проверка статуса контейнеров
Убедитесь, что все контейнеры запущены и работают корректно:
```bash
docker-compose ps
```
Вы должны увидеть статус `Up` для всех сервисов.

## Работа с платформой

### Доступ к платформе
После успешного развертывания платформа будет доступна по адресу вашего домена (например, http://mydomain.com). Стандартные данные для входа в качестве администратора:
- Логин: `admin`
- Пароль: `Admin_12345` (задается в переменной `ADMIN_PASS`)

### Управление пользователями
Чтобы создать нового пользователя или изменить существующего (жюри или администратора) перейдите во вкладку `Настройки`.

### Загрузка файлов
Чтобы загрузить файлы перейдите во вкладку `Файлы`.

### Создание конкурса
Для создания конкурса перейдите во вкладку `Конкурсы` и создайте конкурс с необходимыми блоками.

### Привязка домена
1. Приобретите домен, на котором будет располагаться ваш конкурс, например, чтобы конкурс `Моя земля` был доступен по ссылке `https://my-world.ru` вам необходим домен `my-world.ru`
2. Создайте [A-запись](https://help.reg.ru/support/dns-servery-i-nastroyka-zony/nastroyka-resursnykh-zapisey-dns/nastroyka-resursnykh-zapisey-v-lichnom-kabinete#1) в настройках DNS вашего домена, указывающую на IP-адрес сервера, где развернута платформа
3. Привяжите ваш домен, например `my-world.ru` к существующему конкурсу, который вы создали ранее во вкладке `Домены`.
  
### Просмотр и оценка работ
Для просмотра заявок, вопросов и оценок перейдите во вкладку `Конкурсы` и нажмите на гиперссылку с интересующим разделом.






