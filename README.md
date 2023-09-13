# HotelDePets
Repositório destinado à conter o sistema do hotel de pets
                                                                                                                                    
pra roda tem que ter um postgres no pc colocar a password no appsetings.development.json linha 3 na pasta HDP.API  ex(Adicionar a parte entre as barras [ SEM AS BARRAS ]): "Host=localhost;///Password=michas;///Database=HotelPet;Username=postgres" 

depois vc faz um dotnet ef database update -s HDP.API -p HDP.Persistence  pra gerar o banco no cmd dentro da pasta backend.

dps so rodar dotnet run, ou usar a execução do vs code ou IDE (ver qual a porta q ta rodando o aplicativo e alterar no frontend em enviroment.ts)

apos isso e so dar um npm start no frontend e já vai funcionar do zero
