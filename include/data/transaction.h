#ifndef TRANSACTION_H
#define TRANSACTION_H

#include <string>
#include <cstdint>
#include <chrono>
#include <optional>

class transaction
{
public:
    std::uint64_t id() const;
    void id(std::uint64_t transaction_id);

    std::uint64_t account_id() const;
    void account_id(std::uint64_t acc_id);

    const std::string& label() const;
    void label(std::string transaction_label);

    const std::optional<std::string>& note() const;
    void note(std::optional<std::string> transaction_note);

    const std::optional<std::string>& payment_method() const;
    void payment_method(std::optional<std::string> method);

    const std::optional<std::string>& check_number() const;
    void check_number(std::optional<std::string> ck_number);

    std::chrono::system_clock::time_point created_at() const;
    void created_at(std::chrono::system_clock::time_point creation_time);

    std::chrono::year_month_day accounting_date() const;
    void accounting_date(std::chrono::year_month_day date);

    std::optional<std::chrono::year_month_day> value_date() const;
    void value_date(std::optional<std::chrono::year_month_day> date);

    bool is_reconciled() const;
    void is_reconciled(bool reconciled);

    std::int64_t amount() const;
    void amount(std::int64_t cents);

private:
    std::uint64_t id_{0};
    std::uint64_t account_id_{0};
    std::string label_;
    std::optional<std::string> note_;
    std::optional<std::string> payment_method_;
    std::optional<std::string> check_number_;
    std::chrono::system_clock::time_point created_at_{};
    std::chrono::year_month_day accounting_date_{};
    std::optional<std::chrono::year_month_day> value_date_;
    bool is_reconciled_{false};
    std::int64_t amount_{0};
};

#endif  // TRANSACTION_H

/*
    SQL query to create the table
    
    CREATE TABLE IF NOT EXISTS transactions (
        id              INTEGER PRIMARY KEY,
        account_id      INTEGER NOT NULL REFERENCES account(id) ON DELETE RESTRICT,
        label           TEXT NOT NULL,
        note            TEXT,
        payment_method  TEXT,
        check_number    TEXT,
        created_at      INTEGER NOT NULL,   -- secondes depuis l'epoch
        accounting_date TEXT NOT NULL
            CHECK (accounting_date GLOB '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'),
        value_date      TEXT
            CHECK (value_date IS NULL
                OR value_date GLOB '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'),
        is_reconciled   INTEGER NOT NULL DEFAULT 0 CHECK (is_reconciled IN (0, 1)),
        amount          INTEGER NOT NULL        -- centimes, signé (crédit > 0, débit < 0)
    );

    CREATE INDEX IF NOT EXISTS transactions_account_date
        ON transactions(account_id, accounting_date);
*/