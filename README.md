# DynamicSun тестовое задание

## Автор

Титов Даниил Ярославович
- [Telegram](https://t.me/Menemi)
- [Почта](mailto:danila.titov1210@yandex.ru)

## Выбранные технологии:

- _ASP.Net Core MVC_: `7.0`
- _EntityFrameworkCore_: `7.0.12`
- _NPOI_: `2.6.2`
- _СУБД_: `PostgreSQL`

## Описание

ASP.Net Core MVC приложение для загрузки и отображения архивов погодных условий в городе Москве.
Обе части проекта (backend и frontend) писались вручную, `bootstrap` не был использован.

Состоит из трёх страниц:
- Главная страница, на которой присутствует меню из двух пунктов:
  - Просмотр архивов погодных условий в Москве
  - Загрузка архивов погодных условий в Москве

![главная страница](https://github.com/Menemi/DynamicSun-TestTask/blob/master/screenshots4readme/home.png)
- Страница просмотра архивов погодных условий в Москве c постраничной навигацией и возможностью просмотра погодных условий по:
  - Месяцам
  - Годам

![страница просмотра архивов](https://github.com/Menemi/DynamicSun-TestTask/blob/master/screenshots4readme/view.png)
- Страница для загрузки архивов погодных условий в Москве также с постраничной навигацией. Архив погодных условий представляет собой файл Excel. После загрузки он разбирается и загружается в БД [PostgreSQL]. Есть загружать, как один, так и несколько файлов одновременно. Если файл Excel является "побитым" или данные внутри него являются некорректными, то приложение продолжает свою работу в штатном режиме.

![страница загрузки](https://github.com/Menemi/DynamicSun-TestTask/blob/master/screenshots4readme/input.png)

