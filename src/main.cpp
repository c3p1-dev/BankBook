#include <iostream>
#include <sqlite3.h>
#include <filesystem>

#include "config.h"

// internal functions
namespace
{
    void config_quick_test()
    {
        std::cout << std::endl << "Quick config class test" << std::endl;
        // load configuration file
        config cfg;
        if (!cfg.load("bankbook.conf"))
        {
            std::cout << "Failed to load configuration file." << std::endl;
            std::cout << "Creating default configuration file." << std::endl;

            cfg.set("database_path", "bankbook.db");
            cfg.set("log_level", "debug");
            if (cfg.save("bankbook.conf"))
                std::cout << "Default configuration file created successfully." << std::endl;
            else
                std::cout << "Failed to create default configuration file." << std::endl;
        }
        else
        {
            std::cout << "Configuration loaded successfully" << std::endl;

            // print all key-value pairs
            for (const auto& [key, value] : cfg.get_all())
                std::cout << key << " = " << value << std::endl;

            // then destroy the file
            if (std::filesystem::exists("bankbook.conf"))
            {
                std::cout << "Deleting configuration file." << std::endl;
                std::filesystem::remove("bankbook.conf");
            }
            else
                std::cout << "Configuration file does not exist." << std::endl;
        }
    }

    void sqlite3_quick_test()
    {
        std::cout << std::endl << "Quick sqlite3 test" << std::endl;
        sqlite3* db;
        int rc = sqlite3_open("bankbook.db", &db);

        if (rc)
        {
            std::cout << "Can't open database: " << sqlite3_errmsg(db) << std::endl;
            return;
        }
        else
            std::cout << "Opened database successfully" << std::endl;

        sqlite3_close(db);
        // delete the database file
        if (std::filesystem::exists("bankbook.db"))
        {
            std::cout << "Deleting database file." << std::endl;
            std::filesystem::remove("bankbook.db"); 
        }
    }
}

int main()
{
    //
    std::cout << "Hello bankbook!" << std::endl;

    // quick config class test
    config_quick_test();

    // quick sqlite3 test
    sqlite3_quick_test();

    return 0;
}
