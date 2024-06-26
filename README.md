# Trabalho_IngestaoRadar


Trabalho feito em dupla entre: [Enzo Henrico](https://github.com/EnzoHenrico) e [Guilherme Satilio](https://github.com/gsatilio).

## Design Patterns utilizados

#### Singleton:
Utilizamos o Design Pattern <b>Singleton</b> para garantir uma única <u>instancia</u> de banco de dados, assim declarando no Models as classes relativas ao MsSQL e MongoDB seguindo esse padrão.

#### Proxy (Cache): 
Como segundo Pattern, escolhemos o <b>Proxy</b> por conta da sua adaptabilidade ao código já implementado, sendo um padrão estrutural, colocamos ele entre a camada Services e Repositories fazendo <u>cache</u> das requisições no banco assim melhorando a performance em altas cargas de dados.

## Fluxograma dos Módulos
![Alt text](./Model.drawio.png)
---
![Alt text](./Persist.drawio.png)
---
![Alt text](./ReadAndPersist.drawio.png)
---
![Alt text](./FileGenerator.drawio.png)
