﻿#include <iostream>
#include <vector>
#define _WINSOCK_DEPRECATED_NO_WARNINGS 1
#include "Winsock2.h"
#include <string>
#include "WSAErrors.h"
#pragma comment(lib, "WS2_32.lib")

struct GETSINCHRO {
    char cmd[5];
    int curvalue;
};
struct SETSINCHRO {
    char cmd[5];
    int correction;
};

int main()
{
    setlocale(LC_ALL, "Russian");
    WSADATA wsaData;
    int Tc = 0;
    int ClientCounter = 0;
    try
    {
        if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
            throw SetErrorMsgText("Startup Error:", WSAGetLastError());

        SOCKET clientSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
        if (clientSocket == INVALID_SOCKET) {
            throw SetErrorMsgText("socket Error:", WSAGetLastError());
        }
        else {
            SOCKADDR_IN serverAddres;
            serverAddres.sin_family = AF_INET;
            serverAddres.sin_port = htons(2000);
            serverAddres.sin_addr.s_addr = inet_addr("192.168.251.91");
            int N = 10;
            std::cout << "Введите кол-во сообщений\n";
            std::cin >> N;
            int connected = 20;
            int avgcorr = 0;
            int count = N;
            std::cout << "Введите период запроса на синхронизацию\n";
            while (!(std::cin >> Tc)) {
                std::cin.clear();
                std::cout << "Неправильный ввод" << std::endl;
                std::cout << "Введите число: \n";
                while (std::cin.get() != '\n');
                std::cout << ">>";
            }
            SOCKET server = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
            if (server == INVALID_SOCKET) {
                throw SetErrorMsgText("socket Error:", WSAGetLastError());
            }

            std::vector<int> corrections;
            corrections.reserve(N);

            for (int i = 0; i < N; ++i) {
                std::cout << "Величина счётчика: " << ClientCounter << std::endl;
                std::cout << "Величина ожидания: " << Tc << std::endl;
                Sleep(Tc);
                ClientCounter += Tc;

                GETSINCHRO getS = GETSINCHRO();
                getS.cmd[0] = 'S';
                getS.cmd[1] = 'I';
                getS.cmd[2] = 'N';
                getS.cmd[3] = 'C';
                getS.cmd[4] = '\0';
                getS.curvalue = ClientCounter;
                if (sendto(server, (char*)&getS, sizeof(getS), NULL, (sockaddr*)&serverAddres, sizeof(serverAddres)) == SOCKET_ERROR)
                    throw SetErrorMsgText("sendto Error:", WSAGetLastError());

                SOCKADDR_IN  server_addr2;
                memset(&server_addr2, 0, sizeof(server_addr2));
                int addr_Len2 = sizeof(server_addr2);
                SETSINCHRO setS = SETSINCHRO();
                memset(&setS, 0, sizeof(setS));
                if (recvfrom(server, (char*)&setS, sizeof(setS), NULL, (sockaddr*)&server_addr2, &addr_Len2) == SOCKET_ERROR)
                    throw SetErrorMsgText("recvfrom Error:", WSAGetLastError());
                std::cout << "Величина коррекции: " << setS.correction << std::endl;
                ClientCounter += setS.correction;

                // Исключаем первое значение из среднего
                if (i != 0) {
                    corrections.push_back(setS.correction);
                    avgcorr += setS.correction;
                }
            }

            GETSINCHRO getS = GETSINCHRO();
            getS.cmd[0] = 'D';
            getS.cmd[1] = 'I';
            getS.cmd[2] = 'S';
            getS.cmd[3] = 'C';
            getS.cmd[4] = '\0';
            getS.curvalue = ClientCounter;
            if (sendto(server, (char*)&getS, sizeof(getS), NULL, (sockaddr*)&serverAddres, sizeof(serverAddres)) == SOCKET_ERROR)
                throw SetErrorMsgText("sendto Error:", WSAGetLastError());
            if (closesocket(clientSocket) == SOCKET_ERROR)
                throw SetErrorMsgText("closesocket Error:", WSAGetLastError());
            if (WSACleanup() == SOCKET_ERROR)
                throw SetErrorMsgText("Cleanup Error:", WSAGetLastError());
            if (count > 1)
                std::cout << "average  correction: " << avgcorr / (count - 1); // Рассчитываем среднее без первого значения
            else
                std::cout << "Введено слишком мало сообщений для расчета среднего";
        }
    }
    catch (std::string errorMsgText)
    {
        std::cout << std::endl << errorMsgText;
    }
    system("pause");
}
